import { routerRedux } from 'dva/router';
import { message } from 'antd/lib/index';
import { fetch, changePostCMSStatus, findPost, fetchPostInformation, changeAPostCMSStatus } from '../services/post';
import { TABLE_PAGESIZE } from '../common/constants';

const DEFAULT_CURRENT_PAGE = 1;

export default {
  namespace: 'postManagement', /* should be the same with file name */

  state: {
    posts: [],
    postInformation: {
      id: null,
      user: {},
      title: null,
      description: null,
      images: null,
      address: {},
      createdTime: null,
      updatedTime: null,
      status: null,
      category: {},
    },
    currentPage: DEFAULT_CURRENT_PAGE,
    totals: 0,
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

    * fetchPostInformation({ payload }, { call, put }) {
      const response = yield call(fetchPostInformation, payload);
      if (response) {
        yield put({
          type: 'save',
          payload: {
            postInformation: response,
          },
        });
      }
    },
    * changeAPostCMSStatus({ payload }, { call, put }) {
      const response = yield call(changeAPostCMSStatus, payload);
      if (response) {
        yield put({
          type: 'fetchPostInformation',
          payload,
        });
      }
    },

    * changeStatusPostInformation({ payload }, { call, put }) {
      const response = yield call(changeStatusPost, payload);
      if (response) {
        yield put({
          type: 'fetchPostInformation',
          payload,
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
