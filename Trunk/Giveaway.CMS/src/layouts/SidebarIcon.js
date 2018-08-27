import React from 'react';

const generateIconName = (name, color, size) => {
  if (size === '2x') { return `ic_${color}_${name}@${size}.png`; } else {
    return `ic_${color}_${name}.png`;
  }
};
/**
 *
 * @param name
 * @param size
 * @param state either 'hover' or 'normal'
 * @returns {component}
 * @constructor
 */
const SidebarIcon = ({ name, size, state }) => {
  const color = state === 'normal' ? 'dark' : 'blue';
  const imageSource = `/images/icons/${generateIconName(name, color, size)}`;
  return <img src={imageSource} />;
};

export default SidebarIcon;
