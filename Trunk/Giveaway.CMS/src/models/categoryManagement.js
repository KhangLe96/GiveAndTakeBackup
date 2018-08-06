import { routerRedux } from 'dva/router';
import { message } from 'antd/lib/index';
import { createCategory, fetchCategory, updateCategory, deleteCategory } from '../services/category';

export default {
  namespace: 'categoryManagement', /* should be the same with file name */

  state: {
    categories: [],
    totals: 0,
  },

  effects: {
    * create({ payload }, { call, put }) { },
    * fetch({ payload }, { call, put }) {
      const response = yield call(fetchCategory, payload);
      if (response) {
        yield put({
          type: 'save',
          payload: {
            categories: response.results,
            totals: response.pagination.totals,
          },
        });
      }
    },
    * update({ payload }, { call, put }) { },
    * delete({ payload }, { call, put }) { },
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
