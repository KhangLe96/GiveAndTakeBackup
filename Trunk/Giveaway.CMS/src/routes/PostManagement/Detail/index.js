import React from 'react';
import { connect } from 'dva';
import { Table, Divider, Card, Button, Col } from 'antd';
import { Link } from 'dva/router';
import styles from './index.less';

@connect(({ modals, postManagement }) => ({
  ...modals, ...postManagement,
}))
export default class index extends React.Component {
  state = {};
  componentWillMount() {
    this.props.dispatch({
      type: 'postManagement/fetchPost',
      payload: {},
    });
  }
  navigateToEditPage = () => {
  }

  render() {
    const { posts } = this.props;
    const PostNo = this.props.match.params.id;
    let result = posts ? posts.filter(val => {
      return val.postId == PostNo;
    }) : null;
    const imagePaths = result[0].images;
    console.log(imagePaths, "imagePaths");
    const { loading, postManagement } = this.props;
    return (
      <div>
        <div className="containerHeader">
          <h1>Chi tiết</h1>
          <div className="rightButton">
            <Button type="primary" onClick={() => this.navigateToEditPage()}>
              Delete
          </Button>
          </div>

        </div>
        <div className="containerBody">
          <h3> Post Id        : {result[0].postId}</h3>
          <h3> Title          : {result[0].title} </h3>
          <h3> Description    : {result[0].description} </h3>
          <h3> Address        : {result[0].postAddress} </h3>
          <h3> Image          : {result[0].postImageUrl} </h3>
          <h3> Category       : {result[0].category} </h3>
          <h3> User           : {result[0].userPost} </h3>
          <div>
            {imagePaths.map(imagePath => {
              return <img src={imagePath} alt height="300" width="300"/>
            })}
          </div>
        </div>
      </div>
    );
  }
}
