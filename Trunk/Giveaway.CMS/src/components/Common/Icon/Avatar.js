import React from 'react';
import styles from './Icon.less';

function getIconByKey(name) {
  const icons = {
    avatar: 'img_default_avt_big.png',
    small_avatar: 'img_default_avt.png',
  };
  return icons[name];
}

function Avatar({ name, className }) {
  return (
    <div style={{ display: 'block' }}>
      <img src={`/images/${getIconByKey(name)}`} alt="icon_key" className={styles[className]} />
    </div>
  );
}

export default Avatar;
