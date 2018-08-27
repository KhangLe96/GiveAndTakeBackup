import React from 'react';
import cx from 'classnames';
import styles from './Icon.less';

function getIconByKey(name) {
  const icons = {
    avatar: 'img_default_avt.png',
  };
  return icons[name];
}

function Icon({ name, width, height, className, font_size }) {
  return (
    <span className={styles.chovanhan_icon}>
      <i width={width || 16} height={height || 16} style={{ fontSize: { font_size } }} className={cx(className, 'icon', name)} />
    </span>
  );
}

export default Icon;
