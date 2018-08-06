import request from '../utils/request';

const ROOT_PATH = '/categories';

export function createCategory(params) {
  const options = {
    method: 'PUT',
    body: {
      ...params,
    },
  };
  return request(`${ROOT_PATH}/create`, options);
}

export function fetchCategory(params) {
  const options = {
    method: 'GET',
    body: {
      ...params,
    },
  };
  return request(`${ROOT_PATH}`, options);
}

export function updateCategory(params) {
  const options = {
    method: 'POST',
    body: {
      ...params,
    },
  };
  return request(`${ROOT_PATH}/edit`, options);
}

export function deleteCategory(params) {
  const options = {
    method: 'DELETE',
    body: {
      ...params,
    },
  };
  return request(`${ROOT_PATH}/delete`, options);
}
