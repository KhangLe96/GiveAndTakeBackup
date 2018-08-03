import request from '../utils/request';

import { fakeFetchUser, fakeDeletePost } from '../utils/fakeUtils';
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
  return request('/passport/profile/me', options);
}

export function fetchUser(params) {
  const options = {
    method: 'GET',
    body: {
      ...params,
    },
  };
  // return request('/post/get/', options);

  // fake data
  return fakeFetchUser();
}