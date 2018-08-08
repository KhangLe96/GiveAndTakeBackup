import { routerRedux } from 'dva/router';
import { message } from 'antd/lib/index';
import { fetch, deletePost, changeStatus, getProfile } from '../services/user';
import { TABLE_PAGESIZE } from '../common/constants';

const DEFAULT_CURRENT_PAGE = 1;

export default {
  namespace: 'userManagement', /* should be the same with file name */

  state: {
    users: [],
    userProfile: {},
    currentPage: DEFAULT_CURRENT_PAGE,
    totals: 0,
  },

  effects: {
    * fetch({ payload }, { call, put }) {
      const response = yield call(fetch, payload);
      if (response) {
        yield put({
          type: 'save',
          payload: {
            users: response.results,
            currentPage: (payload.page) ? payload.page : DEFAULT_CURRENT_PAGE,
            totals: response.pagination.totals,
          },
        });
      }
    },
    * changeStatus({ payload }, { call, put }) {
      const response = yield call(changeStatus, payload);
      if (response) {
        yield put({
          type: 'fetch',
          payload: {
            page: payload.page,
            limit: TABLE_PAGESIZE,
          },
        });
      }
    },
    * changeStatusProfile({ payload }, { call, put }) {
      const response = yield call(changeStatus, payload);
      if (response) {
        yield put({
          type: 'getProfile',
          payload,
        });
      }
    },
    * getProfile({ payload }, { call, put }) {
      const response = yield call(getProfile, payload);
      if (response) {
        yield put({
          type: 'save',
          payload: {
            userProfile: response,
          },
        });
      }
    },
  },

  reducers: {
    save(state, action) {
      return {
        ...state,
        ...action.payload,
      };
    },
  },
};
