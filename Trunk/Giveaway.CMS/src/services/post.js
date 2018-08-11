import { stringify } from 'qs';
import request from '../utils/request';

const ROOT_PATH = '/post';

export async function fetch(params) {
  const options = {
    method: 'GET',
    body: {
      ...params,
    },
  };
  return request(`${ROOT_PATH}/cms/list`, options);
}

export async function changePostCMSStatus(params) {
  const { id } = params;
  const options = {
    method: 'PUT',
    body: {
      status: params.statusCMS,
    },
  };
  return request(`${ROOT_PATH}/cms/status/${id}`, options);
}

// export async function findPost(params) {
//   return request(`${ROOT_PATH}?${stringify(params)}`, {
//     method: 'GET',
//   });
// }

export async function fetchPostInformation(params) {
  const { id } = params;
  const options = {
    method: 'GET',
    body: null,
  };
  return request(`${ROOT_PATH}/cms/detail/${id}`, options);
}

export async function getPostsByCategory(categoryID) {
  const options = {
    method: 'GET',
    body: {
      categoryID,
    },
  };
  return request(`${ROOT_PATH}/cms/list`, options);
}

export async function changeAPostCMSStatus(params) {
  const { id } = params;
  const options = {
    method: 'PUT',
    body: {
      status: params.newStatusCMS,
    },
  };
  return request(`${ROOT_PATH}/cms/status/${id}`, options);
}
