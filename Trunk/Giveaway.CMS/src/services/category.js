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

export function getCategories(params) {
  const options = {
    method: 'GET',
    body: {
      ...params,
    },
  };
  return request(`${ROOT_PATH}`, options);
}

export function getACategory(id) {
  const options = {
    method: 'GET',
  };
  return request(`${ROOT_PATH}/${id}`, options);
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

export function deleteCategory(id) {
  const options = {
    method: 'DELETE',
  };
  return request(`${ROOT_PATH}/${id}`, options);
}
