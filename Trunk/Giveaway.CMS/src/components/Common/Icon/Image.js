import React from 'react';
import styles from './Icon.less';

function getIconByKey(name) {
  const icons = {
    profile: 'img_user_profile.png',
    photo_default: 'img_photo_default.png',
    upload_photo: 'img_upload_photo.png',
    img_background_top: 'img_background_top.png',
    img_background_bottom: 'img_background_bottom.png',
  };
  return icons[name];
}

function Image({ name, height, width, className }) {
  return (
    <div style={{ display: 'inline-block' }} >
      <img src={`/images/${getIconByKey(name)}`} height={height} width={width} alt="icon_key" className={styles[className]} />
    </div>
  );
}


export default Image;
