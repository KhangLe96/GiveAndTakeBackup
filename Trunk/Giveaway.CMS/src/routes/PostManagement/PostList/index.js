import React from 'react';
import { Table, Icon, Divider, Card, Button, Spin, Popconfirm } from 'antd';
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
        dataIndex: 'postId',
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
              <Icon type="delete" />
            </Popconfirm>
          </span >),
      },
    ];

  render() {
    return (
      <Table
        columns={this.columns}
        dataSource={this.props.posts}
        pagination={{ pageSize: 10 }}
      />
    );
  }
}
