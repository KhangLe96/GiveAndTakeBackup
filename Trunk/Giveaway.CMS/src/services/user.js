import request from '../utils/request';

export async function queryCurrent() {
  return request('/passport/profile/me');
}

export async function updateProfile() {
  const options = {
    method: 'PUT',
    body: {
      ...params
    },
  };
  return request(`/passport/profile/me`, options);
}
