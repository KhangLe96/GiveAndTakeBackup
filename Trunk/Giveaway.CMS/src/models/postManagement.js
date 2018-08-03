import { routerRedux } from 'dva/router';
import { message } from 'antd/lib/index';
import { fetchPost, deletePost } from '../services/post';

export default {
  namespace: 'postManagement', /* should be the same with file name */

  state: {
    posts: [],
  },

  effects: {
    * fetchPost(payload, { call, put }) {
      const posts = yield call(fetchPost, payload);
      yield put({
        type: 'savePost',
        payload: posts,
      });
    },
    * deletePost({ payload }, { call, put }) {
      const response = yield call(deletePost, payload);
      if (response) {
        const newPayload = { ...payload, ...payload.posts.filter(post => post.postId !== id) };
        yield put({
          type: 'deleteAPost',
          newPayload,
        });
      }
    },
  },

  reducers: {
    savePost(state, action) {
      return {
        ...state,
        posts: action.payload,
      };
    },
    deleteAPost(state, action) {
      const { id, posts } = action.payload;
      return {
        ...state,
        posts: posts.filter(post => post.postId !== id),
      };
    },
  },
};
