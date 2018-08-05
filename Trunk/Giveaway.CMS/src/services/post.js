import request from '../utils/request';
import { stringify } from 'qs';



// fake data
import { fakeFetchPost, fakeDeletePost } from '../utils/fakeUtils';


export async function fetchPost(params) {
  const options = {
    method: 'GET',
    body: {
      ...params,
    },
  };
  return request('/post/getAll', options);
  // fake data
  // return fakeFetchPost();
}

export function deletePost(params) {
  const options = {
    method: 'DELETE',
    body: {
      ...params,
    },
  };
  return request('/post/delete/', options);
  // fake data
  // return fakeDeletePost();
}

export async function findPost(params) {
  console.log(`/post?${stringify(params)}`);
  return request(`/post?${stringify(params)}`, {
    method: 'GET',
  });
}
