import React from 'react';
import { connect } from 'dva';
import { Table, Icon, Divider, Card, Button } from 'antd';
import { Link, routerRedux } from 'dva/router';

import { posts } from '../Auth/FakeData/post.json';

const columns =
  [
    {
      title: 'ID',
      dataIndex: 'postId',
      key: 'postId',
      render: val => <Link to={`/post-management/detail/${val}`}>{val}</Link>,
    },
    {
      title: 'Title',
      dataIndex: 'title',
      key: 'title',
    },
    {
      title: 'Address',
      dataIndex: 'postAddress',
      key: 'postAddress',
    },
    {
      title: 'Category',
      dataIndex: 'category',
      key: 'category',
    }, {
      title: 'Day Post',
      dataIndex: 'dayPost',
      key: 'dayPost',
    },
    {
      title: 'Action',
      dataIndex: 'dayPost',
      render: () => (
        <span>
          <Button onClick>
            <Icon type="delete" />
          </Button>
        </span >),
    },
  ];


@connect(state => ({
  postManagement: state.postManagement,
}))
export default class index extends React.Component {
  state = {};

  render() {
    return (
      <div>
        <div className="containerHeader">
          <h1>Post Management</h1>
        </div>
        <div className="containerBody">
          <Table
            columns={columns}
            dataSource={posts}
            pagination={{ pageSize: 10 }}
          />
        </div>
      </div>
    );
  }
}
