import request from '../utils/request';
import { stringify } from 'qs';

const ROOT_PATH = '/user';

export function fetch(params) {
  const options = {
    method: 'GET',
    body: {
      ...params,
    },
  };
  return request(`${ROOT_PATH}/getList`, options);
}

export async function changeStatus(params) {
  const { id } = params;
  console.log(params);
  const options = {
    method: 'PUT',
    body: {
      status: params.newStatus,
    },
  };
  return request(`${ROOT_PATH}/status/${id}`, options);
}

