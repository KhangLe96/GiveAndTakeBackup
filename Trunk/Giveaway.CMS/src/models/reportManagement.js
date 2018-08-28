import { Table, Button, Popconfirm, message } from 'antd';
import { fetch, createWarningMessage } from '../services/report';
import { TABLE_PAGESIZE } from '../common/constants';


const DEFAULT_CURRENT_PAGE = 1;

export default {
  namespace: 'reportManagement',

  state: {
    reports: [],
    reportInformation: {
      id: null,
      user: {},
      post: {},
      createdTime: null,
      updatedTime: null,
      status: null,
      message: null,
      warningNumber: 0,
    },
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
            reports: response.results,
            currentPage: (payload.page) ? payload.page : DEFAULT_CURRENT_PAGE,
            totals: response.pagination.totals,
          },
        });
      }
    },

    * createWarningMessage({ payload, callback }, { call, put }) {
      const response = yield call(createWarningMessage, payload);
      if (response) {
        callback();
        yield put({
          type: 'fetch',
          payload: {
            limit: TABLE_PAGESIZE,
            page: payload.page || DEFAULT_CURRENT_PAGE,
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
