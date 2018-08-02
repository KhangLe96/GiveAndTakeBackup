import React from 'react';
import { connect } from 'dva';
import PostList from '../CategoryManagement/CategoryList';

@connect(({ modals, postManagement }) => ({
  ...modals, ...postManagement,
}))
export default class index extends React.Component {
  componentDidMount() {
    const { posts, dispatch } = this.props;
    if (posts.length === 0) {
      dispatch({
        type: 'postManagement/fetchPost',
        payload: {},
      });
    }
  }

  render() {
    const { posts } = this.props;
    return (
      <div>
        <div className="containerHeader">
          <h1>Quản lý danh mục</h1>
        </div>
        <div className="containerBody">
          <PostList
            posts={posts}
            dispatch={this.props.dispatch}
          />
        </div>
      </div>
    );
  }
}
