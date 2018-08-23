import React from 'react';
import { Link } from 'dva/router';

import styles from './WeekComponent.less';

const WeekComponent = ({ week, date }) => {
  return (
   <div className={styles.weekBox}>
     <div className={styles.week}>
       <div className={styles.current_week}>Tuần {week}</div>
       <div className={styles.date}>{date}</div>
     </div>
   </div>
  );
};

export default WeekComponent;
