import { routerRedux } from 'dva/router';
import { message } from 'antd/lib/index';
import { fetch, deletePost, findPost } from '../services/post';

export default {
  namespace: 'postManagement', /* should be the same with file name */

  state: {
    posts: [],
  },

  effects: {
    * fetch({ payload }, { call, put }) {
      const response = yield call(fetch, payload);
      if (response) {
        yield put({
          type: 'savePost',
          payload: { posts: response.results },
        });
      }
    },
    * findPost({ payload }, { call, put }) {
      const posts = yield call(findPost, payload);
      if (posts) {
        yield put({
          type: 'savePost',
          payload,
        });
      }
    },
    * delete({ payload }, { call, put }) {
      const response = yield call(deletePost, payload);
      if (response) {
        const newPayload = { ...payload, ...payload.posts.filter(post => post.postId !== id) };
        yield put({
          type: 'deletePost',
          payload,
        });
      }
    },
  },

  reducers: {
    savePost(state, action) {
      return {
        ...state,
        ...action.payload,
      };
    },
    deletePost(state, action) {
      const { id, posts } = action.payload;
      return {
        ...state,
        posts: posts.filter(post => post.postId !== id),
      };
    },
  },
};
