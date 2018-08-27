export default {
  namespace: 'modals',
  state: {
    visible: false,
    loading: false,
    component: '',
  },
  effects: {
    * show({ payload }, { call, put }) {
      yield put({
        type: 'changeLoading',
        payload: true,
      });
      yield put(
        {
          type: 'showModal',
          payload,
        }
      );
      yield put({
        type: 'changeLoading',
        payload: false,
      });
    },
    * hide(_, { put }) {
      yield put(
        {
          type: 'hideModal',
        }
      );
    },
  },
  reducers: {
    showModal(state, action) {
      return {
        ...state,
        visible: true,
        component: action.payload.component,
      };
    },
    changeLoading(state, action) {
      return {
        ...state,
        loading: action.payload,
      };
    },
    hideModal(state, action) {
      return {
        ...state,
        visible: false,
        component: '',
      };
    },
  },
};
