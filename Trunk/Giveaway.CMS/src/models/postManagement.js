import { routerRedux } from 'dva/router';
import { message } from 'antd/lib/index';
import { changePostCMSStatus, fetch, findPost } from '../services/post';

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
          type: 'save',
          payload: { posts: response.results },
        });
      }
    },
    * findPost({ payload }, { call, put }) {
      const posts = yield call(findPost, payload);
      if (posts) {
        yield put({
          type: 'save',
          payload,
        });
      }
    },
    * changeCMSStatus({ payload }, { call, put }) {
      const { id, statusCMS, posts } = payload;
      const response = yield call(changePostCMSStatus, id, statusCMS);
      if (response) {
        const newPosts = posts.map((post) => {
          if (post.id === id) {
            return { ...post, statusCMS };
          }
          return post;
        });
        yield put({
          type: 'save',
          payload: {
            posts: newPosts,
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
