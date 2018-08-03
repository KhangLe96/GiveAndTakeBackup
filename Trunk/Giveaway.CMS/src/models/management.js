import { fetchCategories } from '../services/management';

export default {
  namespace: 'management', /* should be the same with file name */

  state: {
    categories: [],
    posts: [],
    users: [],
  },

  effects: {
    * fetchCategory({ payload }, { call, put }) {
      const response = yield call(fetchCategories, payload);
      if (response) {
        yield put({
          type: 'update',
          payload: { categories: response.results },
        });
      }
    },
  },

  reducers: {
    update(state, action) {
      return {
        ...state,
        ...action.payload,
      };
    },
  },
};
