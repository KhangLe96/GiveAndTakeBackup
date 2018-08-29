import { routerRedux } from 'dva/router';
import { message } from 'antd';
import { fetch, deletePost, changeStatus, getProfile } from '../services/user';
import { TABLE_PAGESIZE } from '../common/constants';
import ErrorHelper from '../helpers/ErrorHelper';

const DEFAULT_CURRENT_PAGE = 1;

export default {
  namespace: 'userManagement',

  state: {
    users: [],
    userProfile: {},
    currentPage: DEFAULT_CURRENT_PAGE,
    totals: 0,
  },

  effects: {
    * fetch({ payload }, { call, put }) {
      const response = yield call(fetch, payload);
      if (ErrorHelper.hasException(response)) {
        message.error(ErrorHelper.extractExceptionMessage(response));
      } else {
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
    * changeStatus({ payload, callback }, { call, put }) {
      const response = yield call(changeStatus, payload);
      if (ErrorHelper.hasException(response)) {
        message.error(ErrorHelper.extractExceptionMessage(response));
      } else {
        message.success('Cập nhật trạng thái thành công!');
        yield put({
          type: 'fetch',
          payload: {
            page: payload.page,
            limit: TABLE_PAGESIZE,
          },
        });
        if (callback) {
          callback();
        }
      }
    },
    * changeStatusProfile({ payload }, { call, put }) {
      const response = yield call(changeStatus, payload);
      if (ErrorHelper.hasException(response)) {
        message.error(ErrorHelper.extractExceptionMessage(response));
      } else {
        yield put({
          type: 'getProfile',
          payload,
        });
      }
    },
    * getProfile({ payload }, { call, put }) {
      const response = yield call(getProfile, payload);
      if (ErrorHelper.hasException(response)) {
        message.error(ErrorHelper.extractExceptionMessage(response));
      } else {
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
