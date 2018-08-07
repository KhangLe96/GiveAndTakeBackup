import request from '../utils/request';

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
  const options = {
    method: 'PUT',
    body: {
      status: params.status,
    },
  };
  return request(`${ROOT_PATH}/status/${id}`, options);
}

