import React from 'react';
import { connect } from 'dva';
import PostList from './PostList';

@connect(({ modals, postManagement }) => ({
  ...modals, ...postManagement,
}))
export default class index extends React.Component {
  componentWillMount() {
    this.props.dispatch({
      type: 'postManagement/fetchPost',
      payload: {},
    });
  }

  render() {
    const { posts } = this.props;
    return (
      <div>
        <div className="containerHeader">
          <h1>Post Management</h1>
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
