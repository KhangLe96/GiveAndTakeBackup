import { routerRedux } from 'dva/router';
import { message } from 'antd/lib/index';
import { createCategory, getCategories, getACategory, updateCategory, changeCategoryCMSStatus, deleteCategory } from '../services/category';
import { getPostsByCategory, fetch } from '../services/post';
import { TABLE_PAGESIZE } from '../common/constants';

const DEFAULT_CURRENT_PAGE = 1;

export default {
  namespace: 'categoryManagement', /* should be the same with file name */

  state: {
    categories: [],
    currentPage: DEFAULT_CURRENT_PAGE,
    selectedCategory: {
      categoryName: null,
      categoryImageUrl: null,
      createdTime: null,
      doesHaveAnyPost: true,
      id: null,
      status: null,
      updatedTime: null,
    },
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
        const { results } = yield call(fetch, {});
        let categoryIdsHavingPosts = results.map(value => value.category.id);
        // remove dupclicated value in categoryIdsHavingPosts
        categoryIdsHavingPosts = categoryIdsHavingPosts.filter((value, index, self) => {
          return self.indexOf(value) === index;
        });
        const newCategories = response.results.map((category) => {
          return { ...category, doesHaveAnyPost: categoryIdsHavingPosts.indexOf(category.id) >= 0 };
        });
        yield put({
          type: 'save',
          payload: {
            categories: newCategories,
            currentPage: (payload.page) ? payload.page : DEFAULT_CURRENT_PAGE,
            totals: response.pagination.totals,
          },
        });
      }
    },
    * getACategory({ payload }, { call, put }) {
      let response = yield call(getACategory, payload.id);
      if (response) {
        let newSelectedCategory = { ...response };
        response = yield call(getPostsByCategory, payload.id);
        const postsByCategory = response.results;
        newSelectedCategory = {
          ...newSelectedCategory,
          doesHaveAnyPost: postsByCategory.length !== 0,
        };
        yield put({
          type: 'save',
          payload: {
            selectedCategory: newSelectedCategory,
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
        //REVIEW: should show message to inform user that he has updated successfully.
        //Should navigate to list page
        yield put({
          type: 'getCategories',
          payload: {},
        });
      }
    },
    * changeCMSStatus({ payload }, { call, put }) {
      const { CMSStatus, id, page } = payload;
      const response = yield call(changeCategoryCMSStatus, CMSStatus, id);
      if (response) {
        // After edit CMS status of a category, refetch catogories and this category from API
        // to get updated information
        yield put({
          type: 'getCategories',
          payload: {
            page: payload.page,
            limit: TABLE_PAGESIZE,
          },
        });
        yield put({
          type: 'getACategory',
          payload: { id },
        });
      }
    },
    * delete({ payload }, { call, put }) {
      const response = yield call(deleteCategory, payload.id);
      if (response) {
        yield put({
          type: 'getCategories',
          payload: {
            page: payload.page,
            limit: TABLE_PAGESIZE,
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
