import React from 'react';
import styles from './reportmanagement.less';
import { connect } from 'dva';
import { Table, Input, Popconfirm, Form, Icon, Divider } from 'antd';

let data = require('../Auth/FakeData/post.json');
// console.log(data);
const columns = [{
  title: 'ID',
  dataIndex: 'postId',
  key: 'postId',
  render: text => <a href="javascript:;">{text}</a>,
  render: (text, record) => {
    let path = `#/reportmanagement/report/${record.postId}`;
    return (
      <span>
        <a href={path}>{text}</a>
      </span >
    );
  }, 
}, {
  title: 'Title',
  dataIndex: 'title',
  key: 'title',
}, {
  title: 'Address',
  dataIndex: 'postAddress',
  key: 'postAddress',
}, {
  title: 'Category',
  dataIndex: 'category',
  key: 'category',
}, {
  title: 'Day Post',
  dataIndex: 'dayPost',
  key: 'dayPost',
}];


@connect(state => ({
  postmanagement: state.dashboard,
}))
class ReportMNG extends React.Component {
  state = {};

  render() {
    const { loading  } = this.props;
    return (<div className={styles.dashboard}>
        <text style={{color:'red', fontSize:20, fontWeight: 'bold'}}> Please click ID number to edit the Report </text>
        <Table columns={columns} dataSource={data.posts} pagination={{ pageSize: 10 }} />
    </div>
    );
  }
}

export default ReportMNG;