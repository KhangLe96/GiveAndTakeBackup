import React from 'react';
import { Button } from 'antd';
import { Link } from 'dva/router';

import styles from './HighlightButton.less';

const HighlightButton = ({ to, text, icon, size, ...others }) => {
  return (
    <div className={styles.highlight_button_container}>
      <Link to={to}>
        <Button icon={icon}>{text}</Button>
      </Link>
    </div>
  );
};

export default HighlightButton;
