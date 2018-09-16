import request from '../utils/request';

const ROOT_API_PATH = '/categories';

export function createCategory(category) {
  const options = {
    method: 'POST',
    body: {
      ...category,
    },
  };
  return request(`${ROOT_API_PATH}/cms`, options);
}

export function getCategories(params) {
  const options = {
    method: 'GET',
    body: {
      ...params,
    },
  };
  return request(`${ROOT_API_PATH}/cms/list`, options);
}

export function getACategory(id) {
  const options = {
    method: 'GET',
  };
  return request(`${ROOT_API_PATH}/cms/${id}`, options);
}

export function updateCategory(category, id) {
  const options = {
    method: 'PUT',
    body: {
      ...category,
    },
  };
  return request(`${ROOT_API_PATH}/cms/${id}`, options);
}

export function changeCategoryCMSStatus(CMSStatus, id) {
  const options = {
    method: 'PUT',
    body: {
      status: CMSStatus,
    },
  };
  return request(`${ROOT_API_PATH}/cms/status/${id}`, options);
}

export function deleteCategory(id) {
  const options = {
    method: 'DELETE',
  };
  return request(`${ROOT_API_PATH}/cms/${id}`, options);
}
