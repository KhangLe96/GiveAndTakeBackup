import React from 'react';
import { Table, Icon, Divider, Card, Button, Spin, Popconfirm, Input } from 'antd';
import { Link } from 'dva/router';

export default class index extends React.Component {

  // /Review: use moment and date format to display a datetime, See example in category management
  // After API fix return status is a string, you need convert that status key to vietnamese :))), update button with text and icon, have divider.
  // pass id to detail page, don't use title
  columns =
    [
      {
        title: 'Tiêu đề',
        dataIndex: 'title',
        key: 'title',
        render: val => <Link to={`/post-management/detail/${val}`}>{val}</Link>,
      },
      {
        title: 'Địa chỉ',
        dataIndex: 'address.provinceCityName',
        key: 'address.provinceCityName',
      },
      {
        title: 'Category',
        dataIndex: 'category.categoryName',
        key: 'category.categoryName',

      }, {
        title: 'Ngày đăng',
        dataIndex: 'createdTime',
        key: 'dayPost',
      }, {
        title: 'Trạng thái',
        dataIndex: 'postStatus',
        key: 'postStatus',
      }, {
        title: 'Hành động',
        dataIndex: 'postId',
        key: 'Action',
        render: id => (
          <span>
            <Popconfirm
              title="Bạn chắc chắn muốn ẩn bài đăng này?"
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
