import request from '../utils/request';

// fake data
import { fakeFetchPost, fakeDeletePost } from '../utils/fakeUtils';


export function fetchPost(params) {
  const options = {
    method: 'GET',
    body: {
      ...params,
    },
  };
  // return request('/post/get/', options);

  // fake data
  return fakeFetchPost();
}

export function deletePost(params) {
  const options = {
    method: 'DELETE',
    body: {
      ...params,
    },
  };
  // return request('/post/delete/', options);

  // fake data
  return fakeDeletePost(params.posts, params.id);
}
