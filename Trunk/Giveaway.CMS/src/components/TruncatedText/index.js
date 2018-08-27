import React from 'react';
import { Tooltip } from 'antd';
import PropTypes from 'prop-types';
import styles from './style.less';
import * as _ from 'lodash/string';

const TruncatedText = ({ text, length, placement }) => {
  placement = placement || 'bottom';
  return (
    <Tooltip placement={placement} title={text}>
          <span>{_.truncate(text, {
            length: length,
            separator: ' ',
          })}
          </span>
    </Tooltip>
  );
};

TruncatedText.propTypes = {
  text: PropTypes.any,
  length: PropTypes.number,
  placement: PropTypes.string,
};

export default TruncatedText;
