import React from 'react';
import { connect } from 'dva';
import { Table, Divider, Card, Button } from 'antd';
import { Link, routerRedux } from 'dva/router';
import styles from './postManagement.less';

const data = require('../Auth/FakeData/post.json');
// console.log(data);
const columns = [{
  title: 'ID',
  dataIndex: 'postId',
  key: 'postId',
  // render: text => <a href="javascript:;">{text}</a>, //Review: don't use a tag, should use Link
  render: val => <Link to={`/post-management/detail/${val}`}>{val}</Link>,
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
}, {
  title: 'Action',
  dataIndex: 'dayPost',
  render: object => (
    <span>
      <Link to={`/post-management/edit/${object && object.id}`}>Edit</Link>
      <Divider type="vertical" />
      <Link to="/">Block</Link>
      <Divider type="vertical" />
      <Link to="/">Delete</Link>
    </span>),
}];


@connect(state => ({
  postManagement: state.postManagement,
}))
// Review: class ReportMNG extends React.Component {
export default class index extends React.Component {
  state = {};

  navigateToCreatePage = () => {
    this.props.dispatch(routerRedux.push('/post-management/create'));
  }

  render() {
    const { loading } = this.props;
    return (<div>
      {/* <text style={{color: 'red', fontSize: 20, fontWeight: 'bold'}}> Please click ID number to edit the Report</text> */}
      <div className="containerHeader">
        <h1>Post Management</h1>
        <div className="rightButton">
          <Button type="primary" onClick={() => this.navigateToCreatePage()}>
              Thêm mới post
          </Button>
        </div>

      </div>
      <div className="containerBody">
        <Table
          columns={columns}
          dataSource={data.posts}
          pagination={{ pageSize: 10 }}
        />
      </div>
    </div>
    );
  }
}
