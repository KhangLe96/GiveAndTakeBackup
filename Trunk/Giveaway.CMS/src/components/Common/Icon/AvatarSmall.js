import React from 'react';
import styles from './Icon.less';

function getIconByKey(name) {
  if (name === '' || name === undefined) {
    return './images/img_default_avt.png';
  }
  return name;
}


function AvatarSmall({name, className}) {
  return (
    <div style={{display: 'block'}}>
      <img src={getIconByKey(name)} alt="icon_key" width={200} className={styles[className]}/>
    </div>
  );
}


export default AvatarSmall;
