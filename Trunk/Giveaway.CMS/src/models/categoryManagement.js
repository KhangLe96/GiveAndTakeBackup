import { routerRedux } from 'dva/router';
import { message } from 'antd/lib/index';
import { createCategory, getCategories, getACategory, updateCategory, deleteCategory } from '../services/category';

const DEFAULT_CURRENT_PAGE = 1;

export default {
  namespace: 'categoryManagement', /* should be the same with file name */

  state: {
    categories: [],
    currentPage: DEFAULT_CURRENT_PAGE,
    selectedCategory: {},
    totals: 0,
  },

  effects: {
    * create({ payload }, { call, put }) {
      const response = yield call(createCategory, payload);
      if (response) {
        yield put(routerRedux.push('/category-management'));
        yield put({
          type: 'getCategories',
          payload: {},
        });
      }
    },
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
    * getACategory({ payload }, { call, put }) {
      const response = yield call(getACategory, payload.id);
      if (response) {
        yield put({
          type: 'save',
          payload: {
            selectedCategory: response,
          },
        });
      }
    },
    * update({ payload }, { call, put }) {
      const response = yield call(updateCategory, payload.category, payload.id);
      if (response) {
        yield put({
          type: 'getACategory',
          payload: { id: payload.id },
        });
        yield put({
          type: 'getCategories',
          payload: {},
        });
      }
    },
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
