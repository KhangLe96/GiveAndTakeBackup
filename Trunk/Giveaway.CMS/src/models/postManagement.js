import { routerRedux } from 'dva/router';
import { message } from 'antd/lib/index';

export default {
  namespace: 'postManagement', /* should be the same with file name   //Review: make attention with the way to named, it should clear to anyone understand easily */

  state: {
    data: {
      list: [],
      pagination: {},
    },
    loading: false,
  },

  effects: {
    * fetch({ payload }, { call, put }) {
    },
  },

  reducers: {
    save(state, action) {
      return {
        ...state,
        data: action.payload,
      };
    },
    changeLoading(state, action) {
      return {
        ...state,
        loading: action.payload,
      };
    },
  },
};
