import { routerRedux } from 'dva/router';
import { message } from 'antd/lib/index';
import { fetchPost, deletePost } from '../services/post';

export default {
  namespace: 'categoryManagement', /* should be the same with file name */

  state: {
    categories: [],
  },

  effects: {
    * fetchCategory(payload, { call, put }) {
      const categories = yield call(fetchPost, payload);
      yield put({
        type: 'saveCategory',
        payload: categories,
      });
    },
    * deletePost({ payload }, { call, put }) {
      const response = yield call(deletePost, payload);
      if (response) {
        yield put({
          type: 'deleteAPost',
          payload,
        });
      }
    },
  },

  reducers: {
    saveCategory(state, action) {
      return {
        ...state,
        categories: action.payload,
      };
    },
    deleteAPost(state, action) {
      const { id, categories } = action.payload;
      return {
        ...state,
        categories: categories.filter(post => post.postId !== id),
      };
    },
  },
};
