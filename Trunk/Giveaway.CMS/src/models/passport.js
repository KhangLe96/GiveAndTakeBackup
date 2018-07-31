import { routerRedux } from 'dva/router';
import { message } from 'antd';
import EventEmitter from 'events';
import { fetchMe, forgotPassword, login, register, updateProfile } from '../services/passport';
import { STATUS_VERIFIED } from '../common/constants';
import ErrorHelper from '../helpers/ErrorHelper';
import { diffInSeconds } from '../helpers/DateTimeHelper';
import { queryProfileMe } from '../services/user';

const emitter = new EventEmitter();

const defaultActivationState = {
  errors: {},
  allowSendConfirm: true,
  retryCount: 0,
  lastSentTime: '',
  nextSentTime: '',
  resendTimerId: 0,
};

export default {
  namespace: 'passport',

  state: {
    list: [],
    loading: false,
    isLoggedIn: false,
    currentUser: {},
    activation: { ...defaultActivationState },
    registerErrors: {},
    loginErrors: {},
  },

  effects: {
    * fetchCurrentUserFromLocalStore(_, { call, put }) {
      const isLoggedIn = localStorage.getItem('isLoggedIn');
      const currentUser = localStorage.getItem('currentUser');
      if (currentUser && isLoggedIn) {
        const response = JSON.parse(currentUser);
        yield put({
          type: 'saveLogin',
          payload: response,
        });
      } else {
        yield put(routerRedux.push('/auth'));
      }
    },
    *createUser({ payload, callback }, { call, put }) {
      yield put({
        type: 'modals/changeLoading',
        payload: true,
      });
      const response = yield call(register, payload);
      yield put({
        type: 'modals/changeLoading',
        payload: false,
      });

      if (ErrorHelper.hasException(response) || !response.token) {
        return yield put({
          type: 'saveRegisterError',
          payload: response,
        });
      }

      localStorage.setItem('isLoggedIn', true);
      localStorage.setItem('currentUser', JSON.stringify(response));
      yield put({
        type: 'saveCurrentUser',
        payload: response,
      });
      yield put({
        type: 'changeLoggedIn',
        payload: true,
      });
      yield put({
        type: 'saveLoginError',
        payload: {},
      });

      yield put(routerRedux.push('/'));
      yield put({
        type: 'modals/hide',
      });
    },
    * login({ payload, callback }, { call, put }) {
      yield put({
        type: 'saveLoginError',
        payload: {},
      });
      yield put({
        type: 'modals/changeLoading',
        payload: true,
      });
      const response = yield call(login, payload);
      yield put({
        type: 'modals/changeLoading',
        payload: false,
      });
      if (response && response.access_token) {
        message.success('Đăng nhập thành công.')
        localStorage.setItem('isLoggedIn', true);
        localStorage.setItem('currentUser', JSON.stringify(response));
        yield put({
          type: 'saveLogin',
          payload: response,
        });
        yield put({
          type: 'saveLoginError',
          payload: {},
        });

        yield put({
          type: 'modals/hide',
        });

        yield put(routerRedux.push('/post-management'));
      } else {
        message.error('Tên đăng nhập hoặc mật khẩu sai.');
      }
      yield put({
        type: 'saveLoginError',
        payload: response,
      });

      if (callback) callback(response);
    },
    * fetchMe(_, { call, put }) {
      yield put({
        type: 'modals/changeLoading',
        payload: true,
      });
      const response = yield call(fetchMe);
      yield put({
        type: 'modals/changeLoading',
        payload: false,
      });
      if (response && response.username !== undefined) {
        yield put({
          type: 'saveProfile',
          payload: response,
        });
      }
    },
    * updateProfile({ payload }, { call, put }) {
      yield put({
        type: 'modals/changeLoading',
        payload: true,
      });
      const response = yield call(updateProfile, payload);
      yield put({
        type: 'modals/changeLoading',
        payload: false,
      });
      if (response && response.username !== undefined) {
        message.info('Cập nhật thông tin thành công.');
        yield put(routerRedux.push('/administrator/profile'));
      } else {
        message.error('Cập nhật thông tin thất bại.');
      }
    },
    * loginFacebook({ payload, callback }, { call, put }) {
      yield put({
        type: 'modals/changeLoading',
        payload: true,
      });
      const response = yield call(loginFacebook, payload);
      yield put({
        type: 'modals/changeLoading',
        payload: false,
      });
      if (response.token !== undefined) {
        localStorage.setItem('isLoggedIn', true);
        localStorage.setItem('currentUser', JSON.stringify(response));
        yield put({
          type: 'saveLogin',
          payload: response,
        });
      } else {
        yield put({
          type: 'saveLoginError',
          payload: response,
        });
      }
      if (callback) callback(response);
    },
    * logout(_, { call, put }) {
      yield put({
        type: 'modals/changeLoading', // spin
        payload: true,
      });
      localStorage.removeItem('isLoggedIn'); // clear login status
      localStorage.removeItem('currentUser'); // clear current User login_tokken

      yield put({
        type: 'notifications/resetFetchingStatusInterval',
      });

      yield put({
        type: 'notifications/resetNotificationStates',
      });
      yield put({
        type: 'modals/changeLoading', // spin
        payload: false,
      });
      yield put(routerRedux.push('/auth/login'));
      yield put({
        type: 'saveLogin',
        payload: {},
      });
    },
    * loginOther(_, { call, put }) {
      yield put({
        type: 'logout',
      });
      yield put({
        type: 'modals/show',
        payload: {
          component: 'login',
        },
      });
    },
    * forgotPassword({ payload, callback }, { call, put }) {
      yield put({
        type: 'modals/changeLoading',
        payload: true,
      });
      const response = yield call(forgotPassword, payload);
      yield put({
        type: 'modals/changeLoading',
        payload: false,
      });
      if (response && response.message !== undefined) {
        message.error(response.message);
      } else {
        yield put({
          type: 'modals/show',
          payload: { component: 'password-recover-confirm' },
        });
      }
    },
    * changePassword({ payload, callback }, { call, put }) {
      yield put({
        type: 'modals/changeLoading',
        payload: true,
      });
      const response = yield call(changePassword, payload);
      yield put({
        type: 'modals/changeLoading',
        payload: false,
      });
      if (!ErrorHelper.hasException(response)) {
        message.success('Bạn đã thay đổi mật khẩu thành công!');
        yield put({
          type: 'modals/hide',
        });
        return yield put({
          type: 'logout',
        });
      }
      message.error(ErrorHelper.extractExceptionMessage(response));
    },
    * resetPassword({ payload, callback }, { call, put }) {
      yield put({
        type: 'modals/changeLoading',
        payload: true,
      });
      const response = yield call(resetPassword, payload);
      yield put({
        type: 'modals/changeLoading',
        payload: false,
      });
      if (ErrorHelper.hasException(response)) {
        return message.error(response.message);
      }
      message.success('Bạn đã thay đổi mật khẩu thành công! Hãy đăng nhập lại với mật khẩu mới.');
      yield put({
        type: 'modals/hide',
      });
      yield put({
        type: 'logout',
      });
    },
    * checkRedirection({ _ }, { call, put, select }) {
      yield put({
        type: 'tripBooking/redirectLastBookItem',
      });
    },
  },

  reducers: {
    saveLogin(state, action) {
      return {
        ...state,
        currentUser: action.payload,
        isLoggedIn: action.payload.token && action.payload.token.length > 0,
      };
    },
    saveProfile(state, action) {
      return {
        ...state,
        currentUser: {
          ...state.currentUser,
          profile: action.payload,
        },
      };
    },
    changeLoading(state, action) {
      return {
        ...state,
        loading: action.payload,
      };
    },
    saveCurrentUser(state, action) {
      return {
        ...state,
        currentUser: action.payload,
        isLoggedIn: action.payload.token && action.payload.token.length > 0,
      };
    },
    saveRegisterError(state, action) {
      return {
        ...state,
        registerErrors: action.payload,
      };
    },
    saveLoginError(state, action) {
      return {
        ...state,
        loginErrors: action.payload,
      };
    },
    changeLoggedIn(state, action) {
      return {
        ...state,
        isLoggedIn: action.payload,
      };
    },
    saveActivationError(state, action) {
      return {
        ...state,
        activation: {
          ...state.activation,
          errors: action.payload,
        },
      };
    },
    changeAllowSendConfirm(state, action) {
      return {
        ...state,
        activation: {
          ...state.activation,
          allowSendConfirm: action.payload,
        },
      };
    },
    changeRetryCount(state, action) {
      return {
        ...state,
        activation: {
          ...state.activation,
          retryCount: action.payload,
        },
      };
    },
    changeSendTime(state, action) {
      return {
        ...state,
        activation: {
          ...state.activation,
          lastSentTime: action.payload.lastSentTime,
          nextSentTime: action.payload.nextSentTime,
        },
      };
    },
    resetActivation(state, action) {
      return {
        ...state,
        activation: {
          ...defaultActivationState,
        },
      };
    },
  },

  subscriptions: {
    setup({ dispatch }) {
      emitter.on('onSaveUserCompleted', () => {
      });
    },
  },
};
