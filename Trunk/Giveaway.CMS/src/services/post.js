import request from '../utils/request';

// fake data
import { fakeFetchPost } from '../utils/fakeUtils';


export function fetchPost(params) {
  const options = {
    method: 'GET',
    body: {
      ...params,
    },
  };
  // return request('/passport/profile/me', options);

  // fake data
  return fakeFetchPost();
}
