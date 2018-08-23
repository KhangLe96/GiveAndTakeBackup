import mockjs from 'mockjs';
import { getRule, postRule } from './mock/rule';
import { imgMap } from './mock/utils';
import { delay } from 'roadhog-api-doc';
import { getProfile } from './mock/profile'

// check using proxy or not
const noProxy = process.env.NO_PROXY === 'true';

const proxy = {
  // value is either Object or Array
  'GET /api/v1/currentUser': {
    $desc: "get current user",
    $params: {
      pageSize: {
        desc: 'pagination',
        exp: 2,
      },
    },
    $body: {
      name: 'Serati Ma',
      avatar: 'https://gw.alipayobjects.com/zos/rmsportal/dRFVcIqZOYPcSNrlJsqQ.png',
      userid: '00000001',
      notifyCount: 12,
    },
  },
  // GET POST can be empty
  'GET /api/v1/users': [{
    key: '1',
    name: 'John Brown',
    age: 32,
    address: 'New York No. 1 Lake Park',
  }, {
    key: '2',
    name: 'Jim Green',
    age: 42,
    address: 'London No. 1 Lake Park',
  }, {
    key: '3',
    name: 'Joe Black',
    age: 32,
    address: 'Sidney No. 1 Lake Park',
  }],
  'GET /api/v1/rule': getRule,
  'POST /api/v1/rule': {
    $params: {
      pageSize: {
        desc: 'pagination',
        exp: 2,
      },
    },
    $body: postRule,
  },
  'GET /api/v1/profiles': getProfile,
};

export default noProxy ? {} : delay(proxy, 1000);
