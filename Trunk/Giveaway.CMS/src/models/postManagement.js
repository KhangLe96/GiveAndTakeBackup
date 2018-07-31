import { routerRedux } from 'dva/router';
import { message } from 'antd/lib/index';

// fake data
const data = require('../Auth/FakeData/post.json');

export default {
  namespace: 'postManagement', /* should be the same with file name */

  state: {
    posts: [],
  },

  effects: {
    // fake Data
    *fetchPostsFromFakeData({ payload, callback }, { call, put }) {

    },
  },

  reducers: {
  },
};
