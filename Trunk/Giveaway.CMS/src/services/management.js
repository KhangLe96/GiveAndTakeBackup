import request from '../utils/request';

export function fetchCategories(params) {
  const options = {
    method: 'GET',
    body: {
      ...params,
    },
  };
  return request('/categories/', options);
}
