import { routerRedux } from 'dva/router';
import { message } from 'antd/lib/index';
import { fetch, deletePost, changeStatus } from '../services/user';

const DEFAULT_CURRENT_PAGE = 1;

export default {
  namespace: 'userManagement', /* should be the same with file name */

  state: {
    users: [],
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
          payload,
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
