import React from 'react';
import { Table, Icon, Divider, Card, Button, Spin, Popconfirm, Input } from 'antd';
import { Link } from 'dva/router';

export default class index extends React.Component {

  columns =
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
        key: 'postAddress'
      },
      {
        title: 'Category',
        dataIndex: 'category',
        key: 'category',

      }, {
        title: 'Day Post',
        dataIndex: 'dayPost',
        key: 'dayPost',
      }, {
        title: 'User',
        dataIndex: 'userPost',
        key: 'userPost',
      }, {
        title: 'Status',
        dataIndex: 'status',
        key: 'status',
      }, {
        title: 'Action',
        dataIndex: 'postId',
        key: 'Action',
        render: id => (
          <span>
            <Popconfirm
              title="Bạn chắc chắn muốn xóa?"
              onConfirm={() => {
                const { dispatch, posts } = this.props;
                dispatch({
                  type: 'postManagement/deletePost',
                  payload: { posts, id },
                });
              }}
            >
              <button>
                <Icon type="delete" />
              </button>
            </Popconfirm>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <Popconfirm
              title="Ban User?"
            >
              <button>
                <Icon type="warning" />
              </button>
            </Popconfirm>
          </span >),
      },
    ];

  render() {
    const { posts } = this.props;
    return (
      <Table
        columns={this.columns}
        dataSource={posts.map((post, key) => { return { ...post, key }; })}
        pagination={{ pageSize: 10 }}
      />
    );
  }
}
