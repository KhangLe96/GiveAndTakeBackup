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
  return request(`${ROOT_PATH}/getListPostCMS`, options);
}

export function changePostCMSStatus(id, statusCMS) {
  const options = {
    method: 'PUT',
    body: {
      status: statusCMS,
    },
  };
  return request(`${ROOT_PATH}/statusCMS/${id}`, options);
}

export async function findPost(params) {
  return request(`${ROOT_PATH}?${stringify(params)}`, {
    method: 'GET',
  });
}

export async function fetchPostInformation(params) {
  const { id } = params;
  const options = {
    method: 'GET',
    body: null,
  };
  return request(`${ROOT_PATH}/getDetail/${id}`, options);
}

export async function changeAPostCMSStatus(params) {
  const { id } = params;
  const options = {
    method: 'PUT',
    body: {
      status: params.newStatusCMS,
    },
  };
  return request(`${ROOT_PATH}/statusCMS/${id}`, options);
}
