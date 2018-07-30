import React from 'react';
import { connect } from 'dva';
import { Table, Divider, Card, Button } from 'antd';
import { Link } from 'dva/router';
import styles from './index.less';

@connect(state => ({
  postManagement: state.postManagement,
}))
export default class index extends React.Component {
  state = {};

  navigateToEditPage = () => {

  }

  render() {
    const { loading, postManagement } = this.props;
    return (<div>
      <div className="containerHeader">
        <h1>Chỉnh sửa sth</h1>
        <div className="rightButton">
          <Button type="primary" onClick={() => this.navigateToEditPage()}>
              Cập nhật
          </Button>
        </div>

      </div>
      <div className="containerBody">
        {'content here'}
      </div>
    </div>
    );
  }
}
