import { routerRedux } from 'dva/router';
import { message } from 'antd/lib/index';
import { fetchPost } from '../services/post';

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
    * deleteAPost(payload, { call, put }) {
      const posts = 0;
      yield put({
        type: 'savePost',
        payload: {},
      });
    },
  },

  reducers: {
    savePost(state, action) {
      return {
        ...state,
        posts: action.payload,
      };
    },
  },
};
