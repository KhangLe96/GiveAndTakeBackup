import React from 'react';
import { connect } from 'dva';
import PostList from './PostList';
import { Input, Select, Button } from 'antd';

const Option = Select.Option;
const Search = Input.Search;

@connect(({ modals, postManagement }) => ({
  ...modals, ...postManagement,
}))
export default class index extends React.Component {
  constructor(props) {
    super(props);
    this.handleChange = this.handleChange.bind(this);
    this.handleSearch = this.handleSearch.bind(this);
  }
  componentDidMount() {
    const { posts, dispatch } = this.props;
    if (posts.length === 0) {
      dispatch({
        type: 'postManagement/fetchPost',
        payload: {},
      });
    }
  }
  data = {};

  handleChange(value) {
    this.data = { ...this.data, field: value };
    console.log(this.data);
  }

  handleSearch(value) {
    this.data = { ...this.data, value };
    console.log(this.data);
  }

  render() {
    const { posts } = this.props;
    return (
      <div>
        <div className="containerHeader">
          <h1>Post Management</h1>
        </div>
        <div>
          <Select
            showSearch
            style={{ width: 200 }}
            placeholder="Select type to filter"
            optionFilterProp="children"
            size="large"
            onChange={this.handleChange}
            filterOption={(input, option) => option.props.children.toLowerCase().indexOf(input.toLowerCase()) >= 0}
          >
            <Option value="postStatus">Status</Option>
            <Option value="categoryName">Category Name</Option>
            <Option value="address">Address</Option>
          </Select>
          <Search
            placeholder="input search text"
            enterButton="Search"
            onSearch={this.handleSearch}
            style={{ width: 500 }}
            size="large"
          />
        </div>
        <br />
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
