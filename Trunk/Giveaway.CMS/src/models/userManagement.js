import { routerRedux } from 'dva/router';
import { message } from 'antd/lib/index';
import { fetchUser, deletePost } from '../services/user';

export default {
  namespace: 'userManagement', /* should be the same with file name */

  state: {
    users: [],
  },

  effects: {
    * fetchUser(payload, { call, put }) {
      const users = yield call(fetchUser, payload);
      yield put({
        type: 'saveUser',
        payload: users,
      });
    },
    * banUser({ payload }, { call, put }) {
      const response = yield call(banUser, payload);
      if (response) {
        yield put({
          type: 'banAUser',
          payload,
        });
      }
    },
  },

  reducers: {
    saveUser(state, action) {
      return {
        ...state,
        users: action.payload,
      };
    },
    banAUser(state, action) {
      const { id, users } = action.payload;
      return {
        ...state,
        users: users.filter(user => user.userId !== id),
      };
    },
  },
};
