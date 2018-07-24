import * as uuid from 'uuid';
import Base64 from './base64';

const formatMoney = (value) => {
  Number.prototype.format = function (n, x, s, c) {
    let re = `\\d(?=(\\d{${x || 3}})+${n > 0 ? '\\D' : '$'})`,
      num = this.toFixed(Math.max(0, ~~n));
    return (c ? num.replace(/\./g, c) : num).replace(new RegExp(re, 'g'), `$&${s || ','}`);
  };
  return value.format(0, 3, '.', '.');
};
const isGuid = (stringToTest) => {
  if (stringToTest[0] === '{') {
    stringToTest = stringToTest.substring(1, stringToTest.length - 1);
  }
  const regexGuid = /^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$/gi;
  return regexGuid.test(stringToTest);
};

const formatSchedule = (value) => {
  const arr = value && value.split(';');
  const key = ['1', '2', '3', '4', '5', '6', '7'];
  const target = ['Thứ 2', 'Thứ 3', 'Thứ 4', 'Thứ 5', 'Thứ 6', 'Thứ 7', 'CN'];
  let temp = '';
  arr && arr.map((data) => {
    const st = data.split(',');
    if (st.length === 3) {
      temp += target[key.indexOf(st[0])];
      temp += `, ${st[1]}, ${st[2]}`;
    }
  });
  return temp;
};

export default {
  formatMoney,
  isGuid,
  formatSchedule,
};
