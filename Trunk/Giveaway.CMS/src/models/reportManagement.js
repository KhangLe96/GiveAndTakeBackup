import { fetch } from '../services/report';
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
