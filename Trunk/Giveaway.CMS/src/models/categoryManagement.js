import { routerRedux } from 'dva/router';
import { message } from 'antd/lib/index';
import { createCategory, getCategories, getCategory, updateCategory, deleteCategory } from '../services/category';

const DEFAULT_CURRENT_PAGE = 1;

export default {
  namespace: 'categoryManagement', /* should be the same with file name */

  state: {
    categories: [],
    currentPage: DEFAULT_CURRENT_PAGE,
    totals: 0,
  },

  effects: {
    * create({ payload }, { call, put }) { },
    * getCategories({ payload }, { call, put }) {
      const response = yield call(getCategories, payload);
      if (response) {
        yield put({
          type: 'save',
          payload: {
            categories: response.results,
            currentPage: (payload.page) ? payload.page : DEFAULT_CURRENT_PAGE,
            totals: response.pagination.totals,
          },
        });
      }
    },
    * getACategory({ payload }, { call, put }) { },
    * update({ payload }, { call, put }) { },
    * delete({ payload }, { call, put }) {
      const response = yield call(deleteCategory, payload.id);
      if (response) {
        yield put({
          type: 'getCategories',
          payload: {},
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
