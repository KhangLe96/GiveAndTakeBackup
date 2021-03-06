import { message } from 'antd';
import { fetch, changePostCMSStatus, findPost, fetchPostInformation, changeAPostCMSStatus } from '../services/post';
import { TABLE_PAGESIZE } from '../common/constants';
import ErrorHelper from '../helpers/ErrorHelper';

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
      statusApp: null,
      statusCMS: null,
      category: {},
    },
    currentPage: DEFAULT_CURRENT_PAGE,
    totals: 0,
  },

  effects: {
    * fetch({ payload }, { call, put }) {
      const response = yield call(fetch, payload);
      if (ErrorHelper.hasException(response)) {
        message.error(ErrorHelper.extractExceptionMessage(response));
      } else {
        yield put({
          type: 'save',
          payload: {
            posts: response.results,
            currentPage: (payload.page) ? payload.page : DEFAULT_CURRENT_PAGE,
            totals: response.pagination.totals,
          },
        });
      }
    },

    * fetchPostInformation({ payload }, { call, put }) {
      const response = yield call(fetchPostInformation, payload);
      if (ErrorHelper.hasException(response)) {
        message.error(ErrorHelper.extractExceptionMessage(response));
      } else {
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
      if (ErrorHelper.hasException(response)) {
        message.error(ErrorHelper.extractExceptionMessage(response));
      } else {
        message.success('Cập nhật trạng thái thành công');
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

    * changePostCMSStatus({ payload }, { call, put }) {
      const response = yield call(changePostCMSStatus, payload);
      if (ErrorHelper.hasException(response)) {
        message.error(ErrorHelper.extractExceptionMessage(response));
      } else {
        message.success('Cập nhật trạng thái thành công');
        yield put({
          type: 'fetch',
          payload: {
            page: payload.page,
            limit: TABLE_PAGESIZE,
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
