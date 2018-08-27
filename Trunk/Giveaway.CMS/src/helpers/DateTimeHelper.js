import moment from 'moment/moment';

const diffInSecondsUntilNow = (target) => {
  const now = moment();
  const last = moment(target);
  return parseInt(now.diff(last) / 1000, 10);
};

const DateTimeHelper = {
  diffInSecondsUntilNow,
};

export default DateTimeHelper;
