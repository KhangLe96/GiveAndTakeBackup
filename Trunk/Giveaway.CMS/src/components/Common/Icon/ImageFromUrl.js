import React from 'react';
import styles from './Icon.less';

function getIconByKey(name) {
  // if (name === '') {
  //   return '/images/img_default_avt.png';
  // }
  return name;
}


function ImageFromUrl({ name, className }) {
  return (
    <div style={{ display: 'inline-block' }}>
      <img src={getIconByKey(name)} alt="icon_key"  className={styles[className]} />
    </div>
  );
}

export default ImageFromUrl;
