import { routerRedux } from 'dva/router';
import { message } from 'antd/lib/index';
import { fetchPost, deletePost, findPost } from '../services/post';

export default {
  namespace: 'postManagement', /* should be the same with file name */

  state: {
    posts: [],
  },

  effects: {
    * fetchPost({ payload }, { call, put }) {
      const posts = yield call(fetchPost, payload);
      if (posts) {
        yield put({
          type: 'savePost',
          payload: posts,
        });
      }
    },
    * findPost({ payload }, { call, put }) {
      const findResponse = yield call(findPost, payload);
      if (findResponse) {
        yield put({
          type: 'findingPost',
          payload: posts,
        });
      }
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
    findingPost(state, action) {
      return {
        ...state,
        posts: action.payload,
      };
    }
  },
};
